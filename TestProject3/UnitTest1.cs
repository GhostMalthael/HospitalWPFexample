using Hospital;
namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEnterUserInAccount()
        {
            //Arrange
            User user = new User(1, "qwe", "qwe", "qwe", "1");
            Repository repository = new Repository();
            //Act 
            repository.ReadFileJson();
            string login = user.Login; string password = user.Password;
            bool result = login == user.Login && password == user.Password;
            repository.EnterInAccount(login, password);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestEnterAdminAccount()
        {
            //Arrange
            User user = new User(0, "admin", "admin", "admin", "admin");
            Repository repository = new Repository();
            //Act 
            repository.ReadFileJson();
            string login = user.Login; string password = user.Password;
            bool result = login == user.Login && password == user.Password;
            repository.EnterInAccount(login, password);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestRegistrationAccount()
        {
            //Arrange
            User user = new User(4, "qwezxc", "qwezxc", "qwezxc", "1");
            Repository repository = new Repository();
            //Act 
            repository.ReadFileJson();
            string name = user.Name; string surname = user.Surname ; string login = user.Login; string password = user.Password;
            bool result = login == user.Login && password == user.Password;
            repository.RegAccount(name, surname,login, password);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestWriteDoctorsInFileJson()
        {
            // Arrange
            Repository repository = new Repository();

            // Act
            repository.ReadFileJson();
            repository.WriteDoctorFileJson("test", Doctor.AllSpecialization.Гастроэнтеролог, "test", @"C:\Users\alexc\Desktop\Basket.png");
            
            // Assert
        }

        [TestMethod]
        public void TestDeleteDoctorsFileJson()
        {
            // Arrange
            Repository repository = new Repository();

            // Act
            repository.ReadFileJson();
            repository.WriteRecordsFileJson(Record.AllService.Первичный, "21/03/2024", Doctor.AllSpecialization.Стоматолог, 1);

            // Assert
        }
    }
}