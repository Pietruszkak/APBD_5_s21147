using APBD_5_s21147.Properties;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMockDb, MockDb>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/pets", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAllPets());
});

app.MapGet("/pets/{id:int}", (int id, IMockDb mockDb) =>
{
    var pet = mockDb.GetOnePet(id);
    if (pet is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(pet);
});

app.MapPost("/pets", (Pet pet, IMockDb mockDb) =>
{
    mockDb.AddPet(pet);
    return Results.Created();
});

app.MapPut("/pets/{id:int}", (int id, Pet pet, IMockDb mockDb) =>
{
    var success = mockDb.EditPet(pet, id);
    if (!success)
    {
        return Results.NotFound();
    }
    
    return Results.Ok();
});

app.MapDelete("/pets/{id:int}", (int id, IMockDb mockDb) =>
{
    var success = mockDb.DeletePet(id);
    if (!success)
    {
        return Results.NotFound();
    }
    
    return Results.Ok();
});

app.MapGet("/pets/{id:int}/appointments", (int id, IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAppointmentsByPet(id));
});

app.MapPost("/appointments", (Appointment appointment, IMockDb mockDb) =>
{
    mockDb.AddAppointment(appointment);
    return Results.Created();
});

app.Run();
