// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Collections;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp;

/// <summary>
/// This image contains other images.
/// </summary>
/// <typeparam name="TPixel">The pixel format.</typeparam>
public sealed class MultiImage<TPixel> : Image, IList<Image<TPixel>>
    where TPixel : unmanaged, IPixel<TPixel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultiImage{TPixel}"/> class.
    /// </summary>
    /// <param name="configuration">The configuration providing initialization code which allows extending the library.</param>
    /// <param name="images">The image collection.</param>
    /// <param name="metadata">The images metadata.</param>
    /// <param name="width">The width of the Image in pixels.</param>
    /// <param name="height">The height of the Image in pixels.</param>
    public MultiImage(
        Configuration configuration,
        IEnumerable<Image<TPixel>> images,
        ImageMetadata metadata,
        int width,
        int height)
        : base(configuration, images.First().PixelType, metadata, width, height)
        => this.Images = images.ToList();

    internal IList<Image<TPixel>> Images { get; }

    /// <inheritdoc/>
    public int Count => this.Images.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => this.Images.IsReadOnly;

    /// <inheritdoc/>
    protected override ImageFrameCollection NonGenericFrameCollection => this.Images.First().Frames;

    /// <inheritdoc/>
    public Image<TPixel> this[int index]
    {
        get => this.Images[index];
        set => this.Images[index] = value;
    }

    /// <inheritdoc/>
    public void Add(Image<TPixel> item) => this.Images.Add(item);

    /// <inheritdoc/>
    public void Clear() => this.Images.Clear();

    /// <inheritdoc/>
    public override Image<TPixel2> CloneAs<TPixel2>(Configuration configuration) => throw new NotImplementedException();

    /// <inheritdoc/>
    public bool Contains(Image<TPixel> item) => this.Images.Contains(item);

    /// <inheritdoc/>
    public void CopyTo(Image<TPixel>[] array, int arrayIndex) => this.Images.CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public IEnumerator<Image<TPixel>> GetEnumerator() => this.Images.GetEnumerator();

    /// <inheritdoc/>
    public int IndexOf(Image<TPixel> item) => this.Images.IndexOf(item);

    /// <inheritdoc/>
    public void Insert(int index, Image<TPixel> item) => this.Images.Insert(index, item);

    /// <inheritdoc/>
    public bool Remove(Image<TPixel> item) => this.Images.Remove(item);

    /// <inheritdoc/>
    public void RemoveAt(int index) => this.Images.RemoveAt(index);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (Image<TPixel> image in this.Images)
            {
                image.Dispose();
            }
        }
    }

    /// <inheritdoc/>
    internal override void Accept(IImageVisitor visitor) => this.Images.First().Accept(visitor);

    /// <inheritdoc/>
    internal override Task AcceptAsync(IImageVisitorAsync visitor, CancellationToken cancellationToken) => this.Images.First().AcceptAsync(visitor, cancellationToken);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Images).GetEnumerator();
}
