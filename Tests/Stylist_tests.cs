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
    public void Test_Save_Find_FoundItemEqualsItem()
    {
      //Arrange/Act
      Stylist item = new Stylist("Sherri", "M-W-F", "Clients love her.");
      item.Save();

      //Assert
      Assert.Equal(Stylist.Find(item.id), item);
    }
    [Fact]
    public void Test_Update()
    {
      //Arrange/Act
      Stylist item = new Stylist("Sherri", "M-W-F", "Clients love her.");
      item.Save();
      item.Update(new List<string>{"name"}, new List<object>{"Sherry"});

      //Assert
      Assert.Equal(Stylist.Find(item.id).name, "Sherry");
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
