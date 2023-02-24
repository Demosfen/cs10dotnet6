using AutoFixture;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public sealed class UnitsCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Box>(composer => composer
            .With(p => p.ProductionDate,
                () => DateTime.MinValue
                    .AddDays(new Random().Next(1020)))
            .With(p => p.ExpiryDate, DateTime.Today)
            .With(p => p.Width, () => new Random().Next(1, 10))
            .With(p => p.Height, () => new Random().Next(1, 10))
            .With(p => p.Depth, () => new Random().Next(1, 10))
            .With(p => p.IsDeleted, false));

        fixture.Customize<Palette>(composer => composer
            .With(p => p.Width, () => new Random().Next(10, 20))
            .With(p => p.Height, () => new Random().Next(10, 20))
            .With(p => p.Depth, () => new Random().Next(10, 20))
            .With(p => p.IsDeleted, false));

        fixture.Customize<Warehouse>(composer => composer
            .With(p => p.Name, "Test Warehouse")
            .With(p => p.IsDeleted, false));
    }
}