﻿using Content.Server._Citadel.Contracts.Systems;

namespace Content.Server._Citadel.Contracts.Components;

/// <summary>
/// This is used for tracking core information about a contract, namely it's status.
/// </summary>
[RegisterComponent, Access(typeof(ContractManagementSystem))]
public sealed class ContractComponent : Component
{
    [DataField("status")]
    public ContractStatus Status;
    [ViewVariables]
    public Mind.Mind OwningContractor = default!;
    [ViewVariables]
    public List<Mind.Mind> SubContractors = new();
}

public enum ContractStatus : uint
{
    /// <summary>
    /// Invalid contract state, if this shows up anywhere except as the "old" status during first setup, something's broken.
    /// </summary>
    Uninitialized = 0,
    /// <summary>
    /// Contract is not yet initiated, i.e. the owning contractor has not yet indicated starting the contract.
    /// This is a prep phase and contract objectives should be flexible at this point, adjusting to match the details as they change.
    /// </summary>
    Initiating,
    /// <summary>
    /// The contract is on-going. Some finalizing criteria may have been met by this point but no breaching criteria have been met.
    /// </summary>
    Active,
    /// <summary>
    /// The contract is finalized, all finalizing ("winning") criteria have been met and rewards should be granted.
    /// </summary>
    Finalized,
    /// <summary>
    /// Any breaching criteria has been met, imposing breach of contract penalties.
    /// </summary>
    Breached,
    /// <summary>
    /// The contract has been cancelled by the contractor(s), imposing cancellation penalties.
    /// </summary>
    Cancelled,
}

/// <summary>
/// A directed event fired upon change in contract status.
/// </summary>
public readonly record struct ContractStatusChangedEvent(ContractStatus Old, ContractStatus New);
