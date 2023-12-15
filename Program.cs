using SenPlus.Builders;
using SenPlus.Handlers;
using SenPlus.Helpers;

SenPlusIoc.Configure();

var App = new SenPlusBuilder(SenPlusIoc.GetBot())
  .AddHandleUpdate()
  .AddHandlePollingError()
  .AddReceivingOptions()
  .AddCommandList()
  .AddCommandMethods()
  .AddDevelopmentLogger()
  .Build();

await App.StartReceivingAsync();