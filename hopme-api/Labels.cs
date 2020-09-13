using Microsoft.ML.OnnxRuntime.Tensors;

public class UserCharacteristics
{
    public float Gender { get; set; }
    public float AgeGroup { get; set; }
    public float EnglishProficiency { get; set; }
    public float HighestEducation { get; set; }

    public Tensor<float> AsTensor()
    {
        float[] data = new float[]
        {
            Gender, AgeGroup, EnglishProficiency, HighestEducation
        };
        int[] dimensions = new int[] { 1, 4 };
        // the tensor object containing the input variables to be used in ONNX model
        return new DenseTensor<float>(data, dimensions);
    }
}