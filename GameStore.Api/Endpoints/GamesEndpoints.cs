using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPoint = "GetGame";

    private static List<GameDto> games = new List<GameDto>
    {
        new GameDto(
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 5)
        ),
        new GameDto(
            2,
            "Final Fantasy XIV",
            "Roleplaying",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new GameDto(
            3,
            "Street Fighter III",
            "Fighting",
            29.99M,
            new DateOnly(1995, 7, 5)
        ),
    };

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group  = app.MapGroup("games").WithParameterValidation();

        // GET GAMES "/games"
        group.MapGet("/", () => games);

        // GET GAME "/games/id"
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndPoint);

        // POST GAME "/games"
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            // if (string.IsNullOrEmpty(newGame.Name)){
            //     return Results.BadRequest("Name is required");
            // }
            GameDto game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);
        });

        // PUT GAME "/games/id"
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE GAME "/games/id"
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}