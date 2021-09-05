using Leopotam.Ecs;

namespace Systems.UI
{
    public class PopUpRewardSystem : IEcsRunSystem
    {

        public void Run()
        {
            //По событию взрыва колец активирует/создает один из анимированных спрайтов с количеством награды (спрайт выбрать из pipeRing даты через тип кольца)
        }
    }
}
