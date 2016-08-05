using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    // override the DB connection to use the test database
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      // Arrange, Act
      int result = Band.GetAll().Count;
      // Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      //Arrange, Act
      Band firstBand = new Band("Best Band");
      Band secondBand = new Band("Best Band");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Band testBand = new Band("Tres Cool Band");
      testBand.Save();

      Band testBand2 = new Band("Tres Cool Band");
      testBand2.Save();
      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

  }
}
