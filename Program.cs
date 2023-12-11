using SenPlus.Builders;
using SenPlus.Handlers;

SenPlusIoc.Configure();

var App = new SenPlusBuilder(SenPlusIoc.GetBot())
  .AddHandleUpdate()
  .AddHandlePollingError()
  .AddReceivingOptions()
  .AddCommands()
  .Build();

await App.StartReceivingAsync();