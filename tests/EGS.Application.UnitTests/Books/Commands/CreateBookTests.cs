using EGS.Application.Books.Commands.CreateBookCommand;
using EGS.Application.Common.Exceptions;
using EGS.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using static EGS.Application.UnitTests.Testing;

namespace EGS.Application.UnitTests.Books.Commands
{
    public class CreateBookTests : TestBase
    {
        [Test]
        public async Task ShouldRequireEssentialFields()
        {
            var command = new CreateBookCommand();

            await FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<ValidationException>();
        }


        [Test]
        public async Task ShouldCreateBook()
        {
            var userId = await RunAsAdministratorAsync();

            var genre = await AddAsync(new BookGenre
            {
                Title = "Biography"
            });

            var willBook = new CreateBookCommand()
            {
                Author = "Will Smith",
                Title = "Will",
                GenreId = genre.Id,
                ISBN = "1984877925",
                Price = 14.99M,
                PublishDate = new DateTime(2021, 1, 13)
            };

            var result = await SendAsync(willBook);
            result.Succeeded.Should().Be(true);

            var book = await FindAsync<Book>(result.Data.Id);
            book.Should().NotBeNull();
            book.Title.Should().BeEquivalentTo(willBook.Title);
            book.ISBN.Should().BeEquivalentTo(willBook.ISBN);
            book.Creator.Should().Be(userId);
        }
    }
}
