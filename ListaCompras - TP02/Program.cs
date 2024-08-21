using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DbListaCompras"));

var app = builder.Build();

//Obter todos os produtos
app.MapGet("/produto", (AppDbContext db) => {
    var todosProdutos = db.Produtos.ToList();

    return Results.Ok(todosProdutos);
});

//Adicionar um produto
app.MapPost("/produto", (Produto umProduto, AppDbContext db) => {
    db.Produtos.Add(umProduto);
    db.SaveChanges();

    return Results.Created($"/produto/{umProduto.Id}", umProduto);
});

//put - atualizar
app.MapPut("/produto/{id}", (int id, Produto umProduto, AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if(produtoExistente is null) return Results.NotFound();

    produtoExistente.Nome = umProduto.Nome;
    produtoExistente.Quantidade = umProduto.Quantidade;
    produtoExistente.Comprado = umProduto.Comprado;

    db.SaveChanges();

    return Results.NoContent();
});

//delete - remover
app.MapDelete("/produto/{id}", (int id, AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if(produtoExistente is null) return Results.NotFound();

    db.Produtos.Remove(produtoExistente);
    db.SaveChanges();

    return Results.NoContent();
});

//getId - Obter 1 produto
app.MapGet("/produto/{id}", (int id, AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if(produtoExistente is null) return Results.NotFound();

    return Results.Ok(produtoExistente);
});

app.Run();
