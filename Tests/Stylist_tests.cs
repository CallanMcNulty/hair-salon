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
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
