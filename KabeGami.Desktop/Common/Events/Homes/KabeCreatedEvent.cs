﻿using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Events.Homes;
internal sealed class KabeCreatedEvent
    : PubSubEvent<IList<KabeDisplayModel>>
{
}