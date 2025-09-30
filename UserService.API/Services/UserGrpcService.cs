using Grpc.Core;
using UserService.Domain.Repositories;
using UserService.GRPC;

namespace UserService.API.Services;

public class UserGrpcService(IUserRepository userRepository) : UserApi.UserApiBase
{
    public override async Task<UserContactInfoResponse> GetUserContactInfo(GetUserContactInfoRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.UserId, out var userGuid))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid GUID format"));
        }

        var user = await userRepository.GetByIdAsync(userGuid, context.CancellationToken);

        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with id {request.UserId} not found"));
        }

        return new UserContactInfoResponse
        {
            Id = userGuid.ToString(),
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty
        };
    }
}
