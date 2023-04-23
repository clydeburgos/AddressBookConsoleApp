
using AddressBook.DataAccess;
using AddressBook.Domain;

string connectionString = "Data Source=.\\LOCALHOST;Initial Catalog=AddressBookDB;Integrated Security=True";
PersonRepository repository = new PersonRepository(connectionString);
Person person = new Person();


Console.WriteLine("Enter First name : ");
person.FirstName = Console.ReadLine();

Console.WriteLine("Enter Last name : ");
person.LastName = Console.ReadLine();

Console.WriteLine("Enter Date of Birth: ");
person.DateOfBirth =  DateTime.Parse(Console.ReadLine().ToString());

Console.WriteLine("Enter Address: ");
person.Address = Console.ReadLine();

SaveRecord(person);

GetAllPersonRecords();

void GetAllPersonRecords() {
    
    var peopleRecords = repository.GetAll();
    foreach (Person personRecord in peopleRecords)
    {
        Console.WriteLine("First Name : " + personRecord.FirstName + ", Last Name : " + personRecord.LastName);
    }
}
void GetPersonRecordById(int id) {
    var personRecord = repository.GetById(id);
    Console.WriteLine("First Name : " + personRecord.FirstName + ", Last Name : " + personRecord.LastName);
}
void SaveRecord(Person personPayload) {
    personPayload.Age = CalculateAge(personPayload.DateOfBirth);
    repository.Add(personPayload);
}

void UpdateRecord(Person personPayload)
{
    repository.Update(personPayload);
}

void DeleteRecord(int id)
{
    repository.Delete(id);
}

bool IsValid(Person personPayload) {
    return personPayload != null && personPayload.FirstName != null && personPayload.LastName != null;
}
int CalculateAge(DateTime dob)
{
    return DateTime.Now.Year - (dob.Year);
}