// using GameStore.Api.Dtos;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// const string GetGameEndPoint = "GetGame";

// List<GameDto> games = [
//     new (
//         1,
//         "Street Fighter II",
//         "Fighting",
//         19.99M, //decimal
//         new DateOnly(1992, 7, 5)
//     ),
//     new (
//         2,
//         "Final Fantasy XIV",
//         "Roleplaying",
//         59.99M,
//         new DateOnly(2010, 9, 30)
//     ),
//     new (
//         3,
//         "Street Fighter III",
//         "Fighting",
//         29.99M,
//         new DateOnly(1995, 7, 5)
//     ),
// ];

// // GET GAMES "/games"
// app.MapGet("games", () => games);

// // GET GAME "/games/id"
// app.MapGet("games/{id}",(int id) => {
//      GameDto? game = games.Find(game => game.Id == id);
//      return game is null ? Results.NotFound() : Results.Ok(game) ;
//     }).WithName(GetGameEndPoint);

// // POST GAME "/game"
// app.MapPost("games", (CreateGameDto newGame) => {
//     GameDto game = new (
//         games.Count + 1,
//         newGame.Name,
//         newGame.Genre,
//         newGame.Price,
//         newGame.ReleaseDate
//     );
//     games.Add(game);

//     return Results.CreatedAtRoute(GetGameEndPoint, new {id = game.Id}, game);
// });

// // PUT GAME "/game/id"
// app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
//     var index = games.FindIndex(game => game.Id == id);

//     if (index == -1){
//         return Results.NotFound();
//     }

//     games[index] = new GameDto(
//         id,
//         updatedGame.Name,
//         updatedGame.Genre,
//         updatedGame.Price,
//         updatedGame.ReleaseDate
//     );

//     return Results.NoContent();
// });

// // DELETE GAME "/games/id"
// app.MapDelete("games/{id}", (int id) => {
//     games.RemoveAll(game => game.Id == id);

//     return Results.NoContent();
// });

// app.MapGet("/", () => "Hello World!");

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
