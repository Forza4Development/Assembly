﻿using System;
using System.Collections.Generic;
using Assembly.Metro.Controls.PageTemplates.Games.Components.MetaComponents;

namespace Assembly.Metro.Controls.PageTemplates.Games.Components.MetaData
{
	/// <summary>
	///     Calculates meta control widths.
	/// </summary>
	public class WidthCalculator : IMetaFieldVisitor
	{
		private const double ReflexiveSubEntryLeftPadding = 20;
		private const double ReflexiveSubEntryRightPadding = 20;
		private const double ReflexiveSubEntryBorderWidth = 1;

		private const double ReflexiveSubEntryExtraWidth =
			ReflexiveSubEntryLeftPadding + ReflexiveSubEntryRightPadding + ReflexiveSubEntryBorderWidth*2;

		private static readonly AsciiValue _asciiControl = new AsciiValue();
		private static readonly Bitfield _bitfieldControl = new Bitfield();
		private static readonly Comment _commentControl = new Comment();
		private static readonly Enumeration _enumControl = new Enumeration();
		private static readonly intValue _intControl = new intValue();
		private static readonly StringIDValue _stringIDControl = new StringIDValue();
		private static readonly rawBlock _rawBlock = new rawBlock();
		//private static MetaChunk _chunkControl = new MetaChunk();
		private static readonly tagValue _tagControl = new tagValue();
		private static readonly VectorValue _vectorControl = new VectorValue();
		private static readonly DegreeValue _degreeControl = new DegreeValue();
		private static readonly ColourValue _colourValue = new ColourValue();
		private static readonly Shader _shader = new Shader();
		private double _totalWidth;

		public double TotalWidth
		{
			get { return _totalWidth; }
		}

		public void VisitBitfield(BitfieldData field)
		{
			AddWidth(_bitfieldControl.Width);
		}

		public void VisitComment(CommentData field)
		{
			AddWidth(_commentControl.Width);
		}

		public void VisitEnum(EnumData field)
		{
			AddWidth(_enumControl.Width);
		}

		public void VisitUint8(Uint8Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitInt8(Int8Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitUint16(Uint16Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitInt16(Int16Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitUint32(Uint32Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitInt32(Int32Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitFloat32(Float32Data field)
		{
			AddWidth(_intControl.Width);
		}

		public void VisitReflexive(ReflexiveData field)
		{
			AddWidth(field.Width);
		}

		public void VisitReflexiveEntry(WrappedReflexiveEntry field)
		{
			// Save our state and recurse into it
			double oldTotal = _totalWidth;
			_totalWidth = 0;
			Add(field.WrappedField);

			double entryWidth = _totalWidth;
			_totalWidth = oldTotal;

			AddWidth(entryWidth + ReflexiveSubEntryExtraWidth);
		}

		public void VisitString(StringData field)
		{
			AddWidth(_asciiControl.Width);
		}

		public void VisitStringID(StringIDData field)
		{
			AddWidth(_stringIDControl.Width);
		}

		public void VisitRawData(RawData field)
		{
			AddWidth(_rawBlock.Width);
		}

		public void VisitDataRef(DataRef field)
		{
			AddWidth(_rawBlock.Width);
		}

		public void VisitTagRef(TagRefData field)
		{
			AddWidth(_tagControl.Width);
		}

		public void VisitVector(VectorData field)
		{
			AddWidth(_vectorControl.Width);
		}

		public void VisitDegree(DegreeData field)
		{
			AddWidth(_degreeControl.Width);
		}

		public void VisitColourInt(ColourData field)
		{
			AddWidth(_colourValue.Width);
		}

		public void VisitColourFloat(ColourData field)
		{
			AddWidth(_colourValue.Width);
		}

		public void VisitShaderRef(ShaderRef field)
		{
			AddWidth(_shader.Width);
		}

		public void Add(MetaField field)
		{
			field.Accept(this);
		}

		public void Add(IEnumerable<MetaField> fields)
		{
			foreach (MetaField field in fields)
				Add(field);
		}


		private void AddWidth(double width)
		{
			_totalWidth = Math.Max(_totalWidth, width);
		}
	}
}