// See https://aka.ms/new-console-template for more information

using EfCoreOneToOne;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (MyDbContext myDbContext = new MyDbContext())
{
    Order order = new Order();
    order.Address = "某某市某某区";
    order.Name = "充电器";
    
    Delivery delivery = new Delivery();
    delivery.CompanyName = "蜗牛快递";
    delivery.Number = "sN4155212345";
    delivery.Order = order;

    // 【注意】这里不能是添加没有外键的Order，否则数据库无法建立起关系，查询会报错
    // myDbContext.Orders.Add(order);
    myDbContext.Deliveries.Add(delivery);
    await myDbContext.SaveChangesAsync();

    var order1 = await myDbContext.Orders.Include(o => o.Delivery).FirstAsync(o => o.Name.Contains("充电器"));
    Console.WriteLine($"名称：{order1.Name},单号：{order1.Delivery.Number}");
}