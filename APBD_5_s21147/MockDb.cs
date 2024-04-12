namespace APBD_5_s21147.Properties;

interface IMockDb
{
    public ICollection<Pet> GetAllPets();
    public bool AddPet(Pet pet);
    public Pet? GetOnePet(int id);
    public bool EditPet(Pet pet, int id);

    public bool DeletePet(int id);
    public ICollection<Appointment> GetAppointmentsByPet(int id);
    public bool AddAppointment(Appointment appointment);

}

public class MockDb : IMockDb
{
    private ICollection<Pet> _pets;
    private ICollection<Appointment> _appointments;

    public MockDb()
    {
        _pets = new List<Pet>();
        _pets.Add(new Pet
        {
            ID=1,
            Name = "Name",
            Category = "Dog",
            Mass = 56.0,
            CoatColor = "Blue"
        });
        _appointments = new List<Appointment>();
    }

    public ICollection<Pet> GetAllPets()
    {
        return _pets;
    }

    public Pet? GetOnePet(int id)
    {
        return _pets.FirstOrDefault(pet => pet.ID==id);
    }
    

    public bool AddPet(Pet pet)
    {
        _pets.Add(pet);
        return true;
    }

    public bool EditPet(Pet pet, int id)
    {
        var petToChange = GetOnePet(id);
        if (petToChange is null)
        {
            return false;
        }
        _pets.Remove(petToChange);
        _pets.Add(pet);
        return true;
    }

    public bool DeletePet(int id)
    {
        var petToDelete = GetOnePet(id);
        if (petToDelete is null)
        {
            return false;
        }
        _pets.Remove(petToDelete);
        return true;
    }

    public ICollection<Appointment> GetAppointmentsByPet(int id)
    {
        return _appointments.Where(appointment => appointment.Pet.ID == id).ToList();
    }

    public bool AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
        return true;
    }
}
