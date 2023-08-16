require(SMV);

setwd(@dir);

let data = open.smv_file("../data/ALS831_lyso_Gd_001.img");

str(smv_header(data));

