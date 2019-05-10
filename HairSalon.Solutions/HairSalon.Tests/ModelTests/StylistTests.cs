using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=kaya;password=kayaspassword;port=8889;database=kaya_jepson_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      // Client.ClearAll();
    }

    [TestMethod]
      public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
      {
        Stylist newStylist = new Stylist("test stylist");
        Assert.AreEqual(typeof(Stylist), newStylist.GetType());
      }
      [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Category";
      Category newCategory = new Category(name);

      //Act
      string result = newCategory.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    // [TestMethod]
    // public void GetId_ReturnsCategoryId_Int()
    // {
    //   //Arrange
    //   string name = "Test Category";
    //   Category newCategory = new Category(name);
    //
    //   //Act
    //   int result = newCategory.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }
    //
    [TestMethod]
  public void GetAll_ReturnsAllCategoryObjects_CategoryList()
  {
    //Arrange
    string name01 = "Work";
    string name02 = "School";
    Category newCategory1 = new Category(name01);
    newCategory1.Save();
    Category newCategory2 = new Category(name02);
    newCategory2.Save();
    List<Category> newList = new List<Category> { newCategory1, newCategory2 };

    //Act
    List<Category> result = Category.GetAll();

    //Assert
    CollectionAssert.AreEqual(newList, result);
  }

    [TestMethod]
    public void Find_ReturnsCorrectCategory_Category()
    {
      //Arrange
      Category testCategory = new Category("household chores");
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.AreEqual(testCategory, foundCategory);
    }

    [TestMethod]
    public void GetItems_ReturnsEmptyItemList_ItemList()
    {
        //Arrange
        string name = "Work";
        Category newCategory = new Category(name);
        List<Item> newList = new List<Item> { };

        //Act
        List<Item> result = newCategory.GetItems();

        //Assert
        CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void AddItem_AssociatesItemWithCategory_ItemList()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Item newItem = new Item(description, 1);
    //   List<Item> newList = new List<Item> { newItem };
    //   string name = "Work";
    //   Category newCategory = new Category(name);
    //   newCategory.AddItem(newItem);
    //
    //   //Act
    //   List<Item> result = newCategory.GetItems();
    //
    //   //Assert
    //   CollectionAssert.AreEqual(newList, result);
    // }

    [TestMethod]
    public void GetItems_RetrievesAllItemsWithCategory_ItemList()
    {
      //Arrange, Act
      Category testCategory = new Category("household chores");
      testCategory.Save();
      Item firstItem = new Item ("Mow the lawn", testCategory.GetId());
      firstItem.Save();
      Item secondItem = new Item("Do the dishes", testCategory.GetId());
      secondItem.Save();
      List<Item>testItemList = new List<Item> {firstItem, secondItem};
      List<Item>resultItemList = testCategory.GetItems();

      //Assert
      CollectionAssert.AreEqual(testItemList, resultItemList);
    }

    [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Category.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    {
      //Arrange, Act
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      //Assert
      Assert.AreEqual(firstCategory, secondCategory);
    }
    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      //Arrange
      Category testCategory = new Category("household chores");
      testCategory.Save();

      //Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      //Arrange
      Category testCategory = new Category("household chores");
      testCategory.Save();

      //Act
      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    }
  }
