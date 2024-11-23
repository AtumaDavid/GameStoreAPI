using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPoint = "GetGame";

List<GameDto> games = [
    new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M, //decimal
        new DateOnly(1992, 7, 5)
    ),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)
    ),
    new (
        3,
        "Street Fighter III",
        "Fighting",
        29.99M,
        new DateOnly(1995, 7, 5)
    ),
];

// GET GAMES "/games"
app.MapGet("games", () => games);

// GET GAME "/games/id"
app.MapGet("games/{id}",(int id) => games.Find(game => game.Id == id)).WithName(GetGameEndPoint);

// POST GAME "/game"
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPoint, new {id = game.Id}, game);
});

// app.MapGet("/", () => "Hello World!");

app.Run();
