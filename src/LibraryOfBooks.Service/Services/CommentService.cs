using AutoMapper;
using LibraryOfBooks.Dataccess.IRepositories;
using LibraryOfBooks.Domain.Entities;
using LibraryOfBooks.Service.DTOs.Comments;
using LibraryOfBooks.Service.Exceptions;
using LibraryOfBooks.Service.Helpers;
using LibraryOfBooks.Service.Interfaces;
using System.Windows.Input;

namespace LibraryOfBooks.Service.Services;

public class CommentService : ICommentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Comment> commentRepository;

    public CommentService(IMapper mapper,
        IRepository<Book> bookRepository,
        IRepository<User> userRepository,
        IRepository<Comment> commentRepository)
    {
        this.mapper = mapper;
        this.bookRepository = bookRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    public async ValueTask<CommentResultDto> AddCommentBookAsync(CommentCreationDto dto)
    {
        var userId = HttpContextHelper.GetUserId();

        var user = await userRepository.SelectAsync(u => u.Id.Equals(userId))
            ?? throw new NotFoundException($"This user not found with {userId}");

        var book = await bookRepository.SelectAsync(b => b.Id.Equals(dto.BookId))
            ?? throw new NotFoundException($"This book not found with {dto.BookId}");

        var mappedComment = this.mapper.Map<Comment>(dto);

        var result = await this.commentRepository.InsertAsync(mappedComment);
        await this.commentRepository.SaveAsync();

        return this.mapper.Map<CommentResultDto>(result);
    }

    public async ValueTask<bool> DeleteCommentBookAsync(long id)
    {
        var existComment = await this.commentRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException($"This comment is not found with id : {id}");

        this.commentRepository.Delete(existComment);
        await this.commentRepository.SaveAsync();

        return true;
    }

    public async ValueTask<CommentResultDto> ModifyCommentBookAsync(CommentUpdateDto dto)
    {
        var comment = await this.commentRepository.SelectAsync(bk => bk.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This comment is not found with id : {dto.Id}");

        this.mapper.Map(dto, comment);

        this.commentRepository.Update(comment);
        await this.commentRepository.SaveAsync();

        return this.mapper.Map<CommentResultDto>(comment);
    }
}
