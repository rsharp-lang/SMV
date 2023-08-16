﻿// export R# package module type define for javascript/typescript language
//
//    imports "Reader" from "SMVFile";
//
// ref=SMVFile.Rscript@SMVFile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
*/
declare namespace Reader {
   module open {
      /**
        * @param env default value Is ``null``.
      */
      function smv_file(file: any, env?: object): any;
   }
}
