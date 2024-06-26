﻿using ErrorOr;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImage;
public sealed record CreateImageCommand(
    string ImageSourcePath,
    string ImageCategory,
    bool IsSFW) : IRequest<ErrorOr<Unit>>;
