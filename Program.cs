using SenPlus.Core;

SenPlusIoc.Configure();

var App = new SenPlusBuilder(SenPlusIoc.GetBot())
  .UseHandleUpdate()
  .UseHandlePollingError()
  .UseReceivingOptions()
  .Build();

await App.StartReceivingAsync();