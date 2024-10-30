using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.Vehicles.Commands.AddInsurance;

internal sealed class AddInsuranceCommandHandler(
    IVehicleRepository vehicleRepository
) : IRequestHandler<AddInsuranceCommand, AddInsuranceResponse>
{
    public async Task<AddInsuranceResponse> Handle(AddInsuranceCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken)
                      ?? throw new VehicleNotFoundException(command.VehicleId);

        var insurance = new InsuranceBuilder()
            .WithTitle(command.Title)
            .WithProvider(command.Provider)
            .WithPolicyNumber(command.PolicyNumber)
            .WithValidFrom(command.ValidFrom)
            .WithValidTo(command.ValidTo)
            .WithVehicle(vehicle)
            .Build();

        vehicle.AddInsurance(insurance);

        await vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);

        return new AddInsuranceResponse(insurance.Id);
    }
}