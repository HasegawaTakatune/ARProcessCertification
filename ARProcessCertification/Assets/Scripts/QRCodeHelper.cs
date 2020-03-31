using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCodeHelper
{
    static public string Read(Texture2D texture)
    {
        BarcodeReader reader = new BarcodeReader();

        int width = texture.width;
        int height = texture.height;
        Color32[] pixel32s = texture.GetPixels32();
        Result result = reader.Decode(pixel32s, width, height);

        return result.Text;
    }

    public static string Read(WebCamTexture texture)
    {
        BarcodeReader reader = new BarcodeReader();

        int width = texture.width;
        int height = texture.height;
        Color32[] pixel32s = texture.GetPixels32();
        Result result = reader.Decode(pixel32s, width, height);

        if (result != null) return result.Text;
        else return "error";
    }

    public static Result Read2(WebCamTexture texture)
    {
        BarcodeReader reader = new BarcodeReader();

        int width = texture.width;
        int height = texture.height;
        Color32[] pixel32s = texture.GetPixels32();
        Result result = reader.Decode(pixel32s, width, height);

        return result;
    }

    public static Texture2D CreateQRCode(string str, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Color32[] content = Write(str, width, height);

        texture.SetPixels32(content);
        texture.Apply();

        return texture;
    }

    public static Color32[] Write(string content, int width, int height)
    {
        Debug.Log(content + "/" + width + "/" + height);

        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = width,
                Height = height
            }
        };

        return writer.Write(content);
    }
}
