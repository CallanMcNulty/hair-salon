using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DeleteAll_StartsEmpty()
    {
      //Arrange
      List<Client> allClients = Client.GetAll();

      //Assert
      Assert.Equal(0, allClients.Count);
    }
    [Fact]
    public void Test_Save_CanRetrieveOneItem()
    {
      //Arrange/Act
      Client item = new Client("Janet", 0, "She's a handful.");
      item.Save();
      List<Client> allClients = Client.GetAll();

      //Assert
      Assert.Equal(1, allClients.Count);
    }
    [Fact]
    public void Test_Save_Find_FoundItemEqualsItem()
    {
      //Arrange/Act
      Client item = new Client("Janet", 0, "She's a handful.");
      item.Save();

      //Assert
      Assert.Equal(Client.Find(item.id), item);
    }
    [Fact]
    public void Test_Update()
    {
      //Arrange/Act
      Client item = new Client("Janet", 0, "She's a handful.");
      item.Save();
      item.Update(new List<string>{"name"}, new List<object>{"Sherry"});
      
      //Assert
      Assert.Equal(item.name, "Sherry");
    }
    [Fact]
    public void Test_Delete()
    {
      //Arrange/Act
      Client item = new Client("Janet", 0, "She's a handful.");
      item.Save();
      item.Delete();

      //Assert
      Assert.Equal(Client.Find(item.id), null);
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}
