require(SMV);
require(graphics2D);

setwd(@dir);

let data = open.smv_file("../data/ALS831_lyso_Gd_001.img");

str(smv_header(data));

let raster = smv_raster(data);

print(raster);

bitmap(file = "./raster_image_heatmap.png", size = [raster]::dimensionSize);

graphics2D::rasterHeatmap(raster, colorName = "viridis");

dev.off();