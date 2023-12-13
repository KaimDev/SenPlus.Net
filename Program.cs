using SenPlus.Builders;
using SenPlus.Handlers;

SenPlusIoc.Configure();

var App = new SenPlusBuilder(SenPlusIoc.GetBot())
  .AddHandleUpdate()
  .AddHandlePollingError()
  .AddReceivingOptions()
  .AddCommandList()
  .AddCommandMethods()
  .Build();

await App.StartReceivingAsync();