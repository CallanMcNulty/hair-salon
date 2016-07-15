using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DeleteAll_StartsEmpty()
    {
      //Arrange
      List<Stylist> allStylists = Stylist.GetAll();

      //Assert
      Assert.Equal(0, allStylists.Count);
    }
    [Fact]
    public void Test_Save_CanRetrieveOneItem()
    {
      //Arrange/Act
      Stylist item = new Stylist("Sherri", "M-W-F", "Clients love her.");
      item.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      //Assert
      Assert.Equal(1, allStylists.Count);
    }
    [Fact]
    public void Test_Save_SavedItemEqualsItem()
    {
      //Arrange/Act
      Stylist item = new Stylist("Sherri", "M-W-F", "Clients love her.");
      item.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      //Assert
      Assert.Equal(allStylists[0], item);
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
