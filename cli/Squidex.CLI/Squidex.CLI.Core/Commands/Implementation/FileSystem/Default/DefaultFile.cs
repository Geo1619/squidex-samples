﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

namespace Squidex.CLI.Commands.Implementation.FileSystem.Default;

public sealed class DefaultFile(FileInfo fileInfo, string fullLocalName) : IFile
{
    public string FullName => fileInfo.FullName;

    public string FullLocalName { get; } = fullLocalName;

    public string Name => fileInfo.Name;

    public long Size
    {
        get => fileInfo.Length;
    }

    public bool Exists
    {
        get => fileInfo.Exists;
    }

    public bool Readonly { get; init; }

    public Stream OpenRead()
    {
        return fileInfo.OpenRead();
    }

    public void Delete()
    {
        if (Readonly)
        {
            throw new InvalidOperationException("The file system is in readonly mode.");
        }

        fileInfo.Delete();
    }

    public Stream OpenWrite()
    {
        if (Readonly)
        {
            throw new InvalidOperationException("The file system is in readonly mode.");
        }

        Directory.CreateDirectory(fileInfo.Directory!.FullName);

        return new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write);
    }

    public override string ToString()
    {
        return FullName;
    }
}
