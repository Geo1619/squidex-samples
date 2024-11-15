﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.ClientLibrary.Utils;

#pragma warning disable SA1629 // Documentation text should end with a period

namespace Squidex.ClientLibrary;

/// <summary>
/// Represents a content query.
/// </summary>
public class ContentQuery
{
    /// <summary>
    /// Gets or sets the IDs of the content items to retrieve.
    /// </summary>
    /// <value>
    /// The IDs of the content items to retrieve.
    /// </value>
    /// <remarks>
    /// If this option is provided all other properties are ignored.
    /// </remarks>
    public HashSet<string>? Ids { get; set; }

    /// <summary>
    /// Gets or sets the JSON query.
    /// </summary>
    /// <value>
    /// The JSON query.
    /// </value>
    /// <remarks>
    /// Do not use this property in combination with OData properties.
    /// </remarks>
    public object? JsonQuery { get; set; }

    /// <summary>
    /// Gets or sets the OData argument to define the number of content items to retrieve (<code>$top</code>).
    /// </summary>
    /// <value>
    /// The the number of content items to retrieve.
    /// </value>
    /// <remarks>
    /// Use this property to implement pagination but not in combination with <see cref="JsonQuery"/> property.
    /// </remarks>
    public int? Top { get; set; }

    /// <summary>
    /// Gets or sets the OData argument to define number of content items to skip (<code>$skip</code>).
    /// </summary>
    /// <value>
    /// The the number of content items to skip.
    /// </value>
    /// <remarks>
    /// Use this property to implement pagination but not in combination with <see cref="JsonQuery"/> property.
    /// </remarks>
    public int? Skip { get; set; }

    /// <summary>
    /// Gets or sets the OData order argument (<code>$orderby</code>).
    /// </summary>
    /// <value>
    /// The OData order argument.
    /// </value>
    /// <remarks>
    /// Do not use this property in combination with <see cref="JsonQuery"/> property.
    /// </remarks>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the OData filter argument (<code>$filter</code>).
    /// </summary>
    /// <value>
    /// The OData filter argument.
    /// </value>
    /// <remarks>
    /// Do not use this property in combination with <see cref="JsonQuery"/> property.
    /// </remarks>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets the OData argument to define number of full text search (<code>$search</code>).
    /// </summary>
    /// <value>
    /// The full text query.
    /// </value>
    public string? Search { get; set; }

    /// <summary>
    /// The locale that is used to compare strings.
    /// </summary>
    public string? Collation { get; set; }

    /// <summary>
    /// Pick a random number of elements from the result set.
    /// </summary>
    public int Random { get; set; }

    internal Query ToQuery(bool supportsSearch, SquidexOptions options)
    {
        var q = Query.Create();

        q.Append("$skip", Skip);
        q.Append("$top", Top);
        q.Append("$orderby", OrderBy);
        q.Append("$filter", Filter);
        q.Append("collation", Collation);
        q.Append("random", Random);
        q.AppendMany("ids", Ids);

        if (JsonQuery != null)
        {
            q.Append("q", JsonQuery.ToJson(options));
        }

        if (!string.IsNullOrWhiteSpace(Search))
        {
            if (!supportsSearch)
            {
                throw new NotSupportedException("Full text search is not supported.");
            }

            q.Append("$search", Search, true);
        }

        return q;
    }
}
