using System.IO;
using GuppyGUI.AbstractDriver;
using System.Reflection;
using System;

namespace GuppyGUI
{
  public class Image
  {
    DriverImage image;


    internal Image(DriverImage img)
    {
      //make it impossible to directly create an image for now
      this.image = img;
    }



    public static Image FromFile(string filename)
    {
      Image res = null;

      FileStream fs = File.OpenRead(filename);
      try
      {
        res = FromStream(fs);
      }
      finally
      {
        fs.Close();
      }
      return res;
    }

    public static Image FromStream(Stream stream)
    {
      DriverImage di;
      try
      {
        di = Guppy.Driver.CreateImage(stream);
      }
      finally
      {

      }

      return new Image(di);
    }

    public static Image FromResource(Assembly asm, string resname)
    {


      //try to get resource with the actual name
      Stream stream = asm.GetManifestResourceStream(resname);
      if (stream == null)
      {
        //get resource with some random namspace ending with the given name
        //this is good for Visual Studio express users, where resources are auto named
        string upname = "." + resname.ToUpper();
        foreach (string s in asm.GetManifestResourceNames())
        {
          if (s.ToUpper().EndsWith(upname))
          {
            stream = asm.GetManifestResourceStream(s);
            break;
          }
        }
      }

      if (stream == null)
        throw new Exception("Resource " + resname + " not found in assembly " + asm.ToString());

      Image res = null;
      try
      {
        res = FromStream(stream);
      }
      finally
      {
        stream.Close();
      }

      return res;

    }

    public static Image FromResource(string resname)
    {
      return FromResource(Assembly.GetCallingAssembly(), resname);
    }


    internal DriverImage DriverObject
    {
      get { return image; }
    }

    public Size2i Size
    {
      get { return new Size2i(image.Width, image.Height); }
    }

  }
}
