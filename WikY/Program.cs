using Microsoft.EntityFrameworkCore;
using WikY.Business;
using WikY.Business.Contracts;
using WikY.Entities;
using WikY.Models.Article;
using WikY.Models.Comment;
using WikY.Repositories;
using WikY.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); ;
builder.Services.AddDbContext<WikYContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("WikYContextCS")));
builder.Services.AddTransient<IArticleRepository, ArticleDbRepository>();
builder.Services.AddTransient<ICommentRepository, CommentDbRepository>();
builder.Services.AddTransient<IArticleBusiness, ArticleBusiness>();
builder.Services.AddTransient<ICommentBusiness, CommentBusiness>();
builder.Services.AddAutoMapper(c =>
{
    c.CreateMap<Article, ArticleViewModel>();
    c.CreateMap<ArticleViewModel, Article>();
    c.CreateMap<Article, ArticleWithCommentsViewModel>();
    c.CreateMap<ArticleWithCommentsViewModel, Article>();
    c.CreateMap<ArticleCreateViewModel, Article>();
    c.CreateMap<Article, ArticleEditViewModel>();
    c.CreateMap<ArticleEditViewModel, Article>();

    c.CreateMap<Comment, CommentViewModel>();
    c.CreateMap<CommentViewModel, Comment>();
    c.CreateMap<CommentCreateViewModel, Comment>();
});

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var appDbContext = serviceScope.ServiceProvider.GetRequiredService<WikYContext>();
    appDbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
