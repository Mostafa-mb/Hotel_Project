using Hotel_Project.Data;
using Hotel_Project.Extention;
using Hotel_Project.ViewModels.StoreProject;
using Microsoft.EntityFrameworkCore;
using Hotel_Project.Models.Basket;
using Hotel_Project.Models.Product;
using NuGet.Packaging;
using Hotel_Project.ViewModels.Account;

namespace Hotel_Project.Core
{
    public class StoreService : IStoreService
    {
        private readonly MyContext _context;

        public StoreService(MyContext context)
        {
            _context = context;
        }

        public List<ShowStoreHotels> ShowStore()
        {
            return _context.hotels.Select(h => new ShowStoreHotels()
            {
                Id = h.Id,
                Description = h.Description,
                ImageName = h.hotelGalleries.First().ImageName,
                Title = h.Title
            }).ToList();
        }

        public ShowSingleHotelVm ShowSingleHotel(long id)
        {
            var hotel = _context.hotels.Include(r => r.hotelRules).Include(g => g.hotelGalleries)
                .Include(x => x.hotelRooms).ThenInclude(r => r.reserveDates).SingleOrDefault(x => x.Id == id);
            return new ShowSingleHotelVm()
            {
                Id = hotel.Id,
                Description = hotel.Description,
                EntryTime = hotel.EntryTime,
                ExitTime = hotel.ExitTime,
                hotelGalleries = hotel.hotelGalleries,
                hotelRules = hotel.hotelRules,
                RoomCount = hotel.RoomCount,
                StageCount = hotel.StageCount,
                Title = hotel.Title,
                hotelRooms = hotel.hotelRooms.Select(x => new RoomListVm()
                {
                    Title = x.Title,
                    BedCount = x.BedCount,
                    Capacity = x.Capacity,
                    Count = x.Count,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    Id = x.Id,
                    RoomPrice = x.RoomPrice,
                    LastReserveDate = x.reserveDates.FirstOrDefault(x => x.ReserveTime.Date >= DateTime.Now.Date),
                    advantagesRoom = _context.advantageToRs.Where(a => a.RoomId == x.Id).Select(a => a.advantageRoom).ToList(),

                }).ToList()
            };
        }

        public ShowSingleRoomVm ShowSingleRoom(long id)
        {
            var room = _context.hotelRooms.Include(r => r.reserveDates).SingleOrDefault(x => x.Id == id);
            if (room != null)
            {
                return new ShowSingleRoomVm()
                {
                    Id = room.Id,
                    Price = room.RoomPrice,
                    BedCount = room.BedCount,
                    Capacity = room.Capacity,
                    Description = room.Description,
                    ImageName = room.ImageName,
                    Title = room.Title,
                    HotelId = room.HotelId,
                    reserveDates = room.reserveDates.Where(x => x.ReserveTime.Date >= DateTime.Now.Date).ToList(),
                    advantageRooms = _context.advantageToRs.Where(x => x.RoomId == room.Id).Select(x => x.advantageRoom).ToList()
                };
            }
            return null;
        }

        public int CreateOrder(GetOrderVm incomeModel)
        {
            try
            {
                var room = _context.hotelRooms.SingleOrDefault(x => x.Id == incomeModel.RoomId);
                if (room == null)
                {
                    return 2;
                }

                var order = _context.Orders.Include(x => x.orderDetails).ThenInclude(r => r.orderReserveDates).
                    SingleOrDefault(x => x.UserId == incomeModel.UserId && !x.IsFinally);
                if (order == null)
                {
                    order = new Order
                    {
                        CreateDate = DateTime.Now,
                        UserId = incomeModel.UserId,
                        HotelId = room.HotelId
                    };

                    _context.Add(order);
                    _context.SaveChanges();

                    var detail = new OrderDetail()
                    {
                        orderId = order.Id,
                        RoomId = room.Id
                    };

                    _context.Add(detail);
                    _context.SaveChanges();

                    var reserveDates = new List<OrderReserveDate>();
                    foreach (var item in incomeModel.Dates)
                    {
                        var date = _context.reserveDates
                            .SingleOrDefault(x => x.RoomId == room.Id && !x.IsReserve && x.Id == item && x.Count > 0);
                        if (date != null)
                        {
                            date.Count -= 1;
                            reserveDates.Add(new OrderReserveDate()
                            {
                                Count = 1,
                                DetailId = detail.Id,
                                Price = date.Price,
                                ReserveId = date.Id
                            });
                            _context.Update(date);
                        }
                    }

                    detail.orderReserveDates = reserveDates;
                    detail.Price = reserveDates.Sum(x => x.Price);
                    order.OrderSum = detail.Price;

                    _context.Update(detail);
                    _context.Update(order);
                    _context.SaveChanges();
                    return 3;
                }
                else
                {
                    var detail = order.orderDetails.SingleOrDefault(x => x.RoomId == incomeModel.RoomId);
                    if (detail != null)
                    {
                        foreach (var item in incomeModel.Dates)
                        {
                            var date = _context.reserveDates
                           .SingleOrDefault(x => x.RoomId == room.Id && !x.IsReserve && x.Id == item && x.Count > 0);
                            if (date != null)
                            {
                                var reserve = detail.orderReserveDates.SingleOrDefault(x => x.ReserveId == date.Id);
                                if (reserve != null)
                                {
                                    date.Count -= 1;
                                    order.OrderSum += date.Price;
                                    detail.Price += date.Price;
                                    reserve.Count += 1;
                                    reserve.Price += date.Price;
                                    _context.Update(date);
                                    _context.Update(order);
                                    _context.Update(reserve);

                                }
                                else
                                {
                                    date.Count -= 1;
                                    detail.orderReserveDates.Add(new OrderReserveDate()
                                    {
                                        Count = 1,
                                        DetailId = detail.Id,
                                        Price = date.Price,
                                        ReserveId = date.Id
                                    });

                                    order.OrderSum += date.Price;
                                    detail.Price += date.Price;
                                    _context.Update(date);
                                    _context.Update(order);
                                }
                            }
                        }
                        _context.Update(detail);
                        _context.SaveChanges();
                        return 4;

                    }
                    else
                    {
                        var orderDetail = new OrderDetail()
                        {
                            RoomId = room.Id,
                            orderId = order.Id
                        };
                        _context.Add(orderDetail);
                        _context.SaveChanges();

                        var reserveDates = new List<OrderReserveDate>();
                        foreach (var item in incomeModel.Dates)
                        {
                            var date = _context.reserveDates
                                .SingleOrDefault(x => x.RoomId == room.Id && !x.IsReserve && x.Id == item && x.Count > 0);
                            if (date != null)
                            {
                                date.Count -= 1;
                                reserveDates.Add(new OrderReserveDate()
                                {
                                    Count = 1,
                                    DetailId = orderDetail.Id,
                                    Price = date.Price,
                                    ReserveId = date.Id
                                });
                                _context.Update(date);
                            }
                        }

                        orderDetail.orderReserveDates = reserveDates;
                        orderDetail.Price = reserveDates.Sum(x => x.Price);
                        order.OrderSum += orderDetail.Price;

                        _context.Update(orderDetail);
                        _context.Update(order);
                        _context.SaveChanges();

                        return 5;
                    }

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public BasketViewModel GetUserBasket(int userId)
        {
            try
            {
                var order = _context.Orders.Include(x => x.orderDetails).ThenInclude(x => x.orderReserveDates).
                    Include(x => x.orderDetails).ThenInclude(x => x.hotelRoom).ThenInclude(x => x.hotel)
                    .SingleOrDefault(x => x.UserId == userId && !x.IsFinally);
                if (order != null)
                {
                    return new BasketViewModel()
                    {
                        OrderSum = order.OrderSum,
                        basketDetails = order.orderDetails.Select(x => new BasketDetailViewModel
                        {
                            BasePrice = x.hotelRoom.RoomPrice,
                            DetailId = x.Id,
                            HotelName = x.hotelRoom.hotel.Title,
                            RoomName = x.hotelRoom.Title,
                            TotalPrice = x.Price,
                            reserveDates = x.orderReserveDates.Select(x => new ReserveDate()
                            {
                                ReserveTime = _context.reserveDates.SingleOrDefault(r => r.Id == x.ReserveId).ReserveTime,
                                Price = x.Price,
                                Id = x.ReserveId
                            }).ToList()
                        }).ToList()
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CheckoutViewModel GetUserCheckout(int userId)
        {
            try
            {
                var order = _context.Orders.Include(x => x.orderDetails).ThenInclude(x => x.orderReserveDates).
                    Include(x => x.orderDetails).ThenInclude(x => x.hotelRoom).ThenInclude(x => x.hotel)
                    .SingleOrDefault(x => x.UserId == userId && !x.IsFinally);
                if (order != null)
                {
                    return new CheckoutViewModel()
                    {
                        OrderSum = order.OrderSum,
                        basketDetails = order.orderDetails.Select(x => new BasketDetailViewModel
                        {
                            BasePrice = x.hotelRoom.RoomPrice,
                            DetailId = x.Id,
                            HotelName = x.hotelRoom.hotel.Title,
                            RoomName = x.hotelRoom.Title,
                            TotalPrice = x.Price,
                            reserveDates = x.orderReserveDates.Select(x => new ReserveDate()
                            {
                                ReserveTime = _context.reserveDates.SingleOrDefault(r => r.Id == x.ReserveId).ReserveTime,
                                Price = x.Price,
                                Id = x.ReserveId
                            }).ToList()
                        }).ToList()
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int PaymentOrder(int userId , CheckoutViewModel incomeModel)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(x => x.UserId == userId && !x.IsFinally);
                if(string.IsNullOrEmpty(incomeModel.Name) || string.IsNullOrEmpty(incomeModel.LastName)
                    || incomeModel.PassCode == 0 || incomeModel.Count == 0)
                {
                    return 1;
                }

                order.IsFinally = true;
                order.Name = incomeModel.Name;
                order.LastName = incomeModel.LastName;
                order.PassCode = incomeModel.PassCode;
                order.Count = incomeModel.Count;
                _context.SaveChanges();
                return 2;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public int RemoveDetail(int id)
        {
            var detail = _context.OrderDetails.Include(x => x.orderReserveDates).SingleOrDefault(x => x.Id == id);
            if(detail != null)
            {
                foreach(var item in detail.orderReserveDates)
                {
                    var date = _context.reserveDates.SingleOrDefault(x => x.Id == item.ReserveId);
                    if(date != null)
                    {
                        date.Count += item.Count;
                    }
                }

                var order = _context.Orders.Include(x => x.orderDetails).SingleOrDefault(x => x.Id == detail.orderId);
                if(order != null)
                { 
                    if(order.orderDetails.Count() <= 1)
                    {
                        _context.Remove(order);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.Remove(detail);
                        _context.SaveChanges();
                    }

                    return 2;
                }
                return 1;
            }
            return 0;
        }

        public ICollection<UserOrdersViewModel> UserOrders(int userId)
        {
            var orders = _context.Orders.Where(x => x.UserId == userId);
            return orders.Select(x => new UserOrdersViewModel()
            {
                HotelName = _context.hotels.SingleOrDefault(h=>h.Id == x.HotelId).Title,
                OrdeId = x.Id,
                OrderSum = x.OrderSum
            }).ToList();
        }
    }
}
