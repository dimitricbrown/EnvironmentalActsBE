using EABackEnd.Models;
using EABackEnd;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7195") // Change to match your SWAGGER Url *****
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
// CHANGE THE "DBCONTEXT" and "DBConnectionString" ******* DELETE THIS LINE AFTERWARDS *******
builder.Services.AddNpgsql<EADbContext>(builder.Configuration["EADbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

//Add for Cors
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//ENDPOINTS 

//USER
//  check user
app.MapGet("/checkuser/{uid}", (EADbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});
// get all users
app.MapGet("/users", (EADbContext db) =>
{
    return db.Users.ToList();
});
// get user by Id
app.MapGet("/users/{id}", (EADbContext db, int id) =>
{
    var user = db.Users.Where(u => u.Id == id);
    return user;
});
//create User 
app.MapPost("/users", (EADbContext db, User User) =>
{
    db.Users.Add(User);
    db.SaveChanges();
    return Results.Created($"/user/{User.Id}", User);
});
// update a user
app.MapPut("/users/{id}", (int id, EADbContext db, User User) =>
{
    User userToUpdate = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }
    userToUpdate.FirstName = User.FirstName;
    userToUpdate.LastName = User.LastName;
    userToUpdate.Email = User.Email;
    db.SaveChanges();
    return Results.Ok(userToUpdate);
});
// delete a user
app.MapDelete("/users/{id}", (EADbContext db, int id) =>
{
    User userToDelete = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToDelete == null)
    {
        return Results.NotFound();
    }
    db.Users.Remove(userToDelete);
    db.SaveChanges();
    return Results.NoContent();
});
// events a user has signed up for 
app.MapGet("/userEvents/{uid}", async (EADbContext db, string uid) =>
{
    var userEvents = await db.Events
        .Where(e => e.Users.Any(u => u.Uid == uid))
        .Include(e => e.Users)
        .Include(e => e.Category)
        .ToListAsync();

    return Results.Ok(userEvents);
});
// events a user has created 
app.MapGet("/userEvents/{createdBy}", async (EADbContext db, string createdBy) =>
{
    var userEvents = await db.Events
        .Where(e => e.Users.Any(u => u.Uid == createdBy))
        .Include(e => e.Users)
        .Include(e => e.Category)
        .ToListAsync();

    return Results.Ok(userEvents);
});


//  EVENT ENDPOINTS
// get users on a event 
app.MapGet("/eventUser/{id}", (EADbContext db, int id) =>
{
    var eventUsersToGet = db.Events.Where(e => e.Id == id).Include(u => u.Users).ToList();
    return eventUsersToGet;
});
// add a user to a event 
app.MapPost("/eventUser/{userId}/{eventId}", (int userId, int eventId, EADbContext db) =>
{
    var user = db.Users.Include(u => u.Events).FirstOrDefault(e => e.Id == eventId);

    if (user == null)
    {
        return Results.NotFound();
    }

    var eventToAdd = db.Events.FirstOrDefault(e => e.Id == eventId);

    if (eventToAdd == null)
    {
        return Results.NotFound();
    }

    user.Events.Add(eventToAdd);
    db.SaveChanges();

    return Results.Ok();
});
// get Events
app.MapGet("/events", async (EADbContext db) =>
{
    var events = await db.Events
    .Include(e => e.Users)
    .Include(e => e.Category)
    .ToListAsync();

    return Results.Ok(events);
});
// get events by id
app.MapGet("/events/{id}", (EADbContext db, int id) =>
{
    var events = db.Events.Find(id);
    if (events == null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(events);
});
//update a Event
app.MapPut("/events/{id}", (int id, EADbContext db, Event Event) =>
{
    Event eventToUpdate = db.Events.SingleOrDefault(o => o.Id == id);
    if (eventToUpdate == null)
    {
        return Results.NotFound();
    }
    eventToUpdate.ScheduledDate = Event.ScheduledDate;
    eventToUpdate.CategoryId = Event.CategoryId;
    eventToUpdate.Description = Event.Description;
    eventToUpdate.Title = Event.Title;
    db.SaveChanges();
    return Results.Ok(eventToUpdate);
});
// Create a Event
app.MapPost("/events", (EADbContext db, Event Event) =>
{
    db.Events.Add(Event);
    db.SaveChanges();
    return Results.Created($"/events/{Event.Id}", Event);
});
//delete a order 
app.MapDelete("/events/{id}", (EADbContext db, int id) =>
{
    Event eventToDelete = db.Events.SingleOrDefault(e => e.Id == id);
    if (eventToDelete == null)
    {
        return Results.NotFound();
    }
    db.Events.Remove(eventToDelete);
    db.SaveChanges();
    return Results.NoContent();
});
// delete an event from a user
app.MapDelete("/eventUser/{eventId}/{userId}", (int eventId, int userId, EADbContext db) =>
{
    var user = db.Users.Include(u => u.Events).FirstOrDefault(u => u.Id == userId);

    if (user == null)
    {
        return Results.NotFound();
    }

    var eventToRemove = db.Events.FirstOrDefault(e => e.Id == eventId);

    if (eventToRemove == null)
    {
        return Results.NotFound();
    }

    user.Events.Remove(eventToRemove);
    db.SaveChanges();

    return Results.Ok("Event removed from user successfully");
});
// Misc Endpoint 
//  get all categories
app.MapGet("/categories", (EADbContext db) =>
{
    return db.Categories.ToList();
});

app.Run();

