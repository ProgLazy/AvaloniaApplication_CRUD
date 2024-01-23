using System;
using System.Runtime.InteropServices.JavaScript;

namespace AvaloniaApplication1.Script_C_;

public class Scripts
{
    public int id { get; set; }
    public int stancia { get; set; }
    public DateTime data_vre { get; set; }
    public string temperature { get; set; }
    public string vlash_H2 { get; set; }
    public string sp_vetra { get; set; }
    public string napravlenie { get; set; }
    public string atmos_dav { get; set; }
    public string oblach { get; set; }
    public string nalichie_osad { get; set; }
}

public class Svoystva_vesh
{
    public int id1 { get; set; }
    public int zagryz_ve { get; set; }
    public string average_sutoch { get; set; }
    public int pdk_m { get; set; }
    public int pdk_rab { get; set; }
    public string pdk_poch { get; set; }
    public int cl_opasnos { get; set; }
    public string plots { get; set; }
    public string temperature { get; set; }
}

public class monitor
{
    public int id2 { get; set; }
    public int stanc { get; set; }
    public int gryaz { get; set; }
    public DateTime data_pr { get; set; }
    public int value_con { get; set; }
}

public class stream_vbr
{
    public int id3 { get; set; }
    public string name_st { get; set; }
    public string type_st { get; set; }
    public int zagryaz_d { get; set; }
    public int ustanov { get; set; }
    public int techno_proc { get; set; }
    public string valov_mos { get; set; }
    public int temp_v { get; set; }
    public int sp_v { get; set; }
    public string ob_ras { get; set; }
    public string dolya_ves { get; set; }
}

public class seeding
{
    public int id9 { get; set; }
    public string log { get; set; }
    public string pass { get; set; }
}

public class filter_Meteo
{
    public int id4 { get; set; }
    public string napravl { get; set; }
}
