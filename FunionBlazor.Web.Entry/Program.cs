//Serve.Run(RunOptions.Default.WithArgs(args));
using FunionBlazor.Web.Entry;
Serve.Run(RunOptions.Default.AddWebComponent<MASAWebComponent>());

