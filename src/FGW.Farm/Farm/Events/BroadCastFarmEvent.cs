﻿using FGW.Core.Farm.Entities.Interfaces;

namespace FGW.Core.Farm.Events;

public struct BroadCastFarmEvent(IResource Resource) : IBaseEvent;