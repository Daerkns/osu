// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Objects;
using osu.Game.Modes.Square.Objects.Drawable;

namespace osu.Game.Modes.Square.Objects
{
    public class SquareHitObject : HitObject
    {
        public int Column;
        public int Row;

        //todo: Proper timing windows
        //todo: Late/early
        public double HitWindowFor(SquareHitResult result)
        {
            switch (result)
            {
                default:
                    return 300;
                case SquareHitResult.Early:
                    return 150;
                case SquareHitResult.Good:
                    return 80;
                case SquareHitResult.Perfect:
                    return 30;
            }
        }

        public SquareHitResult HitResultForOffset(double offset)
        {
            if (offset < HitWindowFor(SquareHitResult.Perfect))
                return SquareHitResult.Perfect;
        	if (offset < HitWindowFor(SquareHitResult.Good))
        		return SquareHitResult.Good;
        	if (offset < HitWindowFor(SquareHitResult.Early))
        		return SquareHitResult.Early;
        	return SquareHitResult.Miss;
        }
    }
}
