kernel void img_trans (global uchar4* inputImg, global uchar4* outputImg)
{
    //adapted from: http://sourceforge.net/p/cloo/discussion/1048266/thread/cb07b92b/#97c7
    int gid = get_global_id(0);
    // Even though the Bitmap format will be ARGB this array will be parsed as BGRA. So be careful with that. No, I don't know why it works like this. Ask Microsoft :)
    uchar4 vectorIn = inputImg[gid];
    uint gray = vectorIn.x * 0.07 + vectorIn.y * 0.72 + vectorIn.z * 0.21;
    outputImg[gid] = (uchar4)(gray,gray,gray, (uchar)255); // This is full red/half transparent color.
}