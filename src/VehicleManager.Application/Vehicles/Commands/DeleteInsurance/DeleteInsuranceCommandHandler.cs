using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.Vehicles.Commands.DeleteInsurance;

internal sealed class DeleteInsuranceCommandHandler(
    IVehicleRepository vehicleRepository
) : IRequestHandler<DeleteInsuranceCommand>
{
    public async Task Handle(DeleteInsuranceCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken)
                      ?? throw new VehicleNotFoundException(command.VehicleId);

        var insurance = vehicle.Insurances.FirstOrDefault(i => i.Id == command.InsuranceId)
                        ?? throw new InsuranceNotFoundException(command.InsuranceId);

        vehicle.RemoveInsurance(insurance);

        await vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);
    }
}