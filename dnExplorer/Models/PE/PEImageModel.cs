﻿using System;
using System.Collections.Generic;
using System.Drawing;
using dnExplorer.Trees;
using dnlib.DotNet.MD;
using dnlib.PE;

namespace dnExplorer.Models {
	public class PEImageModel : LazyModel {
		public IPEImage Image { get; set; }
		public ImageCor20Header CLIHeader { get; set; }

		public PEImageModel(IPEImage peImage, ImageCor20Header cliHeader) {
			Image = peImage;
			CLIHeader = cliHeader;
			Text = "PE Image";
		}

		protected override bool HasChildren {
			get { return true; }
		}

		protected override bool IsVolatile {
			get { return false; }
		}

		protected override IEnumerable<IDataModel> PopulateChildren() {
			yield return new PESectionsModel(Image);
			yield return new PEDDModel(Image);
			if (CLIHeader != null)
				yield return new PECLIModel(Image, CLIHeader);
		}

		public override bool HasIcon {
			get { return true; }
		}

		public override void DrawIcon(Graphics g, Rectangle bounds) {
			g.DrawImageUnscaledAndClipped(Resources.GetResource<Image>("Icons.folder.png"), bounds);
		}
	}
}