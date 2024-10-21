using MediatR;
using Microsoft.EntityFrameworkCore;
using TODOList.Application;
using TODOList.Application.TODO.Commands;
using TODOList.Application.TODO.DTOs;
using TODOList.Application.TODO.Queries;
using TODOList.Domain.Entities;

namespace TODOList.Endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/todos", async (IMediator mediator) =>
            {
                var todos = await mediator.Send(new GetAllTodosQuery()); // Przesyłamy zapytanie do MediatR
                return Results.Ok(todos); // Zwracamy wyniki
            });

            app.MapGet("/todo/{id}", async (int id, IMediator mediator) =>
            {
                var todo = await mediator.Send(new GetTodoByIDQuery(id)); 
                if (todo is null) return Results.NotFound($"Not found todo record on ID: {id}"); 
                return Results.Ok(todo);
            });

            app.MapGet("/todo/{dateFrom}/{dateTo}", async (DateTime dateFrom, DateTime dateTo, IMediator mediator) =>
            {
                var todo = await mediator.Send(new GetTodoByDatesQuery(dateFrom,dateTo)); 
                return Results.Ok(todo);
            });

            app.MapPost("/todo", async (CreateTodoDto todo, IMediator mediator) =>
            {
                string error = string.Empty;
                if (!ValidateTodo(todo, out error)) return Results.BadRequest(error);

                var result = await mediator.Send(new CreateNewTodoCommand(todo));
                if (result > 0) return Results.Created($"/todo/{result}", todo);
                return Results.BadRequest("Error while creating new todo record");
            });
            //app.MapGet("/todos/{id}", async (int id, TodoContext db) =>
            //    await db.Todos.FindAsync(id) is Todo todo
            //        ? Results.Ok(todo)
            //        : Results.NotFound());

                    //app.MapPost("/todos", async (Todo todo, TodoContext db) =>
                    //{
                    //    if (!ValidateTodo(todo))
                    //        return Results.BadRequest("Invalid Todo data");

                    //    db.Todos.Add(todo);
                    //    await db.SaveChangesAsync();
                    //    return Results.Created($"/todos/{todo.Id}", todo);
                    //});

                    //app.MapPut("/todos/{id}", async (int id, Todo updatedTodo, TodoContext db) =>
                    //{
                    //    var todo = await db.Todos.FindAsync(id);
                    //    if (todo is null) return Results.NotFound();

                    //    todo.Title = updatedTodo.Title;
                    //    todo.Description = updatedTodo.Description;
                    //    todo.ExpiryDate = updatedTodo.ExpiryDate;
                    //    todo.PercentComplete = updatedTodo.PercentComplete;

                    //    await db.SaveChangesAsync();
                    //    return Results.NoContent();
                    //});

                    //app.MapDelete("/todos/{id}", async (int id, TodoContext db) =>
                    //{
                    //    var todo = await db.Todos.FindAsync(id);
                    //    if (todo is null) return Results.NotFound();

                    //    db.Todos.Remove(todo);
                    //    await db.SaveChangesAsync();
                    //    return Results.NoContent();
                    //});
        }

        private static bool ValidateTodo(CreateTodoDto todo, out string error)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(todo.Title))
            {
                errorMessage += "Title is required.\n";
            }
            if (string.IsNullOrWhiteSpace(todo.Description))
            {
                errorMessage += "Description is required.\n";
            }
            if (todo.ExpiryDate == DateTime.MinValue)
            {
                errorMessage += "Expiry date is required.\n";
            }
            if (todo.ExpiryDate < DateTime.Today)
            {
                errorMessage += $"The expiry date cannot be less than {DateTime.Today}\n";
            }
            error = errorMessage;
            return string.IsNullOrWhiteSpace(errorMessage);
        }
    }

}
