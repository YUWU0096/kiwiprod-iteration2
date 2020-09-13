using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace hopme_api.Controllers
{
    [ApiController]
    [Route("findjobs")]
    public class InferenceController : ControllerBase
    {
        private readonly InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            // inference session instance as defined in Startup.cs
            _session = session;
        }

        /* POST
         * Only post action is accepted
         * returns a list of categorical labels
         */

        [HttpPost]
        public ActionResult Post(UserCharacteristics data)
        {
            // the four float variables will produce a categorical (string) output
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<string> score = result.First().AsTensor<string>();
            var prediction = new Prediction { PredictedValue = score.GetValue(0).ToString()};
            result.Dispose();
            return Ok(prediction);
        }
    }
}