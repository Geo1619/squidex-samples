// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System.Linq;
using Squidex.ClientLibrary;

namespace Sample.Blog.Models
{
    public sealed class BlogPost : Content<BlogPostData>
    {
        public string GetImageUrl()
        {
            var assetId = Data.Image?.FirstOrDefault();
            return assetId is null
                ? string.Empty
                : $"https://cloud.squidex.io/api/assets/{assetId}";
        }
    }
}
