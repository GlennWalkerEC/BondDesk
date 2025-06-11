using BondDesk.Domain.Entities;
using BondDesk.Domain.Interfaces.Models;
using BondDesk.Domain.Interfaces.Repos;
using BondDesk.Domain.Interfaces.Providers;
using BondDesk.Domain.Interfaces.Entities;
using Moq;
using Xunit;

namespace BondDesk.Domain.Tests.Entities;

public class BondTests
{
    private readonly Mock<IQuoteRepo> _quoteRepoMock = new();
    private readonly Mock<IGiltInfo> _giltInfoMock = new();
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly Mock<IBondQuoteData> _bondQuoteDataMock = new();

    private Bond CreateBond()
    {
        _giltInfoMock.SetupGet(x => x.FaceValue).Returns(1000m);
        _giltInfoMock.SetupGet(x => x.Name).Returns("Test Bond");
        _giltInfoMock.SetupGet(x => x.Coupon).Returns(5m);
        _giltInfoMock.SetupGet(x => x.MaturityDate).Returns(new DateTime(2030, 1, 1));
        _giltInfoMock.SetupGet(x => x.Epic).Returns("TSTBND");
        _giltInfoMock.SetupGet(x => x.CouponPeriodMonths).Returns(6);

        _dateTimeProviderMock.Setup(x => x.GetToday()).Returns(new DateTime(2025, 1, 1));

        _bondQuoteDataMock.SetupGet(x => x.LastPrice).Returns(950m);
        _quoteRepoMock.Setup(x => x.BondValuation(It.IsAny<string>()))
            .ReturnsAsync(_bondQuoteDataMock.Object);

        return new Bond(_quoteRepoMock.Object, _giltInfoMock.Object, _dateTimeProviderMock.Object);
    }

    [Fact]
    public void Constructor_NullQuoteRepo_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Bond(null!, _giltInfoMock.Object, _dateTimeProviderMock.Object));
    }

    [Fact]
    public void Constructor_NullGiltInfo_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Bond(_quoteRepoMock.Object, null!, _dateTimeProviderMock.Object));
    }

    [Fact]
    public void Constructor_NullDateTimeProvider_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Bond(_quoteRepoMock.Object, _giltInfoMock.Object, null!));
    }

    [Fact]
    public void Properties_ReturnExpectedValues()
    {
        var bond = CreateBond();
        Assert.Equal(1000m, bond.FaceValue);
        Assert.Equal("Test Bond", bond.Name);
        Assert.Equal(0.05m, bond.Coupon); // 5m / 100
        Assert.Equal(new DateTime(2030, 1, 1), bond.MaturityDate);
        Assert.Equal("TSTBND", bond.Epic);
    }

    [Fact]
    public void Tenor_ComputesCorrectly()
    {
        var bond = CreateBond();
        // (2030-01-01 - 2025-01-01).Days / 365m = 1826 / 365 = 5.005...
        Assert.Equal(1826m / 365m, bond.Tenor, 3);
    }

    [Fact]
    public void DaysSinceLastCoupon_ComputesCorrectly()
    {
        var bond = CreateBond();
        // Last coupon before 2025-01-01 is 2024-07-01, so days = 184
        Assert.Equal(184m, bond.DaysSinceLastCoupon);
    }

    [Fact]
    public void LastPrice_ReturnsFromQuoteData()
    {
        var bond = CreateBond();
        Assert.Equal(950m, bond.LastPrice);
    }

    [Fact]
    public void DirtyPrice_ComputesCorrectly()
    {
        var bond = CreateBond();
        // accruedInterest = (1000 * 0.05 * 184) / 182 = 5050 / 182 = 27.802...
        // dirtyPrice = 950 + 27.802...
        var expected = 950m + (1000m * 0.05m * 184m) / 182m;
        Assert.Equal(expected, bond.DirtyPrice, 3);
    }

    [Fact]
    public void RunningYield_ComputesCorrectly()
    {
        var bond = CreateBond();
        // Coupon * FaceValue / LastPrice * FaceValue = 0.05 * 1000 / 950 * 1000
        var expected = 0.05m * 1000m / 950m * 1000m;
        Assert.Equal(expected, bond.RunningYield, 3);
    }

    [Fact]
    public void YieldToMaturity_Computes()
    {
        var bond = CreateBond();
        var ytm = bond.YieldToMaturity;
        Assert.True(ytm > 0);
    }

    [Fact]
    public void GetValuation_ReturnsQuoteData()
    {
        var bond = CreateBond();
        var result = bond.GetValuation();
        Assert.Equal(_bondQuoteDataMock.Object, result);
    }

    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        var bond = CreateBond();
        var expected = "Test Bond (TSTBND) - 0.05% coupon, matures on 01/01/2030";
        Assert.Equal(expected, bond.ToString());
    }
}