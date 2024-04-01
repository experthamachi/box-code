var distances = items
			.Select(i => new ItemDistance<T>(i, i.Envelope.DistanceTo(x, y)))
			.OrderBy(i => i.Distance)
			.AsEnumerable();

		if (maxDistance.HasValue)
			distances = distances.TakeWhile(i => i.Distance <= maxDistance.Value);

		if (predicate != null)
			distances = distances.Where(i => predicate(i.Item));

		if (k > 0)
			distances = distances.Take(k);

		return distances
			.Select(i => i.Item)
			.ToList();
	}

	/// <summary>
	/// Calculates the distance from the borders of an <see cref="Envelope"/>
	/// to a given point.
	/// </summary>
	/// <param name="envelope">The <see cref="Envelope"/> from which to find the distance</param>
	/// <param name="x">The x-coordinate of the given point</param>
	/// <param name="y">The y-coordinate of the given point</param>
	/// <returns>The calculated Euclidean shortest distance from the <paramref name="envelope"/> to a given point.</returns>
	public static double DistanceTo(this in Envelope envelope, double x, double y)
	{
		var dX = AxisDistance(x, envelope.MinX, envelope.MaxX);
		var dY = AxisDistance(y, envelope.MinY, envelope.MaxY);
		return Math.Sqrt((dX * dX) + (dY * dY));

		static double AxisDistance(double p, double min, double max) =>
		   p < min ? min - p :
		   p > max ? p - max :
		   0;
