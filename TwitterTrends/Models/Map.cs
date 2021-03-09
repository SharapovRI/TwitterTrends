using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
	class Map
	{
		public static float max_el(Coordinate p, Coordinate r)
		{
			float max = r.X;
			if (p.X > r.X) max = p.X;
			return max;
		}
		public static float min_el(Coordinate p, Coordinate r)
		{
			float min = r.X;
			if (p.X < r.X) min = p.X;
			return min;
		}

		public static bool ontheSide(Coordinate p, Coordinate q, Coordinate r)
		{
			if (q.X <= max_el(p, r) && q.X >= min_el(p, r) && q.Y <= max_el(p, r) && q.Y >= min_el(p, r))
				return true;
			return false;
		}
		public static int orientation(Coordinate p, Coordinate q, Coordinate r)
		{
			float val = (q.Y - p.Y) * (r.X - q.X) -
				(q.X - p.X) * (r.Y - q.Y);

			if (val == 0) return 0;  // colinear 
			return (val > 0) ? 1 : 2; // clock or counterclock wise 
		}
		public static bool Intersection(Coordinate p, Coordinate pn, Coordinate pr, Coordinate q2)
		{
			int o1 = orientation(p, pn, pr);
			int o2 = orientation(p, pn, q2);
			int o3 = orientation(pr, q2, p);
			int o4 = orientation(pr, q2, pn);
			if (o1 != o2 && o3 != o4)
				return true;
			if (o1 == 0 && ontheSide(p, pr, pn)) return true;
			if (o2 == 0 && ontheSide(p, q2, pn)) return true;
			if (o3 == 0 && ontheSide(pr, p, q2)) return true;
			if (o4 == 0 && ontheSide(pr, pn, q2)) return true;

			return false;
		}
		public static bool isInside(List<Coordinate> pt, int n, Coordinate ranD)
		{
			if (n < 3) return false; // меньше 3-ёх вершин - не фигура 


			Coordinate extreme = new Coordinate(float.PositiveInfinity, ranD.Y); // создаем точку на бесконечности 


			int count = 0, i = 0;
			do
			{
				int next = (i + 1) % n; // проходим через все вершины фигуры


				if (Intersection(pt[i], pt[next], ranD, extreme))
				{
					if (orientation(pt[i], ranD, pt[next]) == 0)
						return ontheSide(pt[i], ranD, pt[next]);

					count++;
				}
				i = next;
			} while (i != 0);
			if (count % 2 == 0) return false;
			return true;
		}
	}


}

