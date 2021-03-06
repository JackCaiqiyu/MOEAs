﻿using MOEAPlat.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEAPlat.Problems
{
    public class ZDT1 : AbstractMOP
    {
        private static ZDT1 instance;
        private ZDT1(int pd)
        {
            this.parDimension = pd;
            Init();
        }
        public override void Evaluate(MoChromosome chromosome)
        {
            double[] sp = chromosome.realGenes;
            double[] obj = chromosome.objectivesValue;

            for (int i = 0; i < this.parDimension; i++)
                sp[i] = domain[i, 0] + sp[i] * (domain[i, 1] - domain[i, 0]);

            obj[0] = sp[0];
            double g = gf(sp);
            double part2 = (1 - Math.Sqrt(sp[0] / g));
            obj[1] = g * part2;

            for (int i = 0; i < this.parDimension; i++)
                sp[i] = (sp[i] - domain[i, 0]) / (domain[i, 1] - domain[i, 0]);
        }

        private double gf(double[] point)
        {
            double sum = 0;
            for (int i = 1; i < parDimension; i++)
                sum += point[i];
            return 1 + 9 * sum / (parDimension - 1);
        }

        public override void Init()
        {
            this.domain = new double[this.parDimension, 2];
            for (int i = 0; i < parDimension; i++)
            {
                domain[i, 0] = 0;
                domain[i, 1] = 1;
            }
            this.objDimension = 2;
            this.range = new double[objDimension, 2];
        }

        public static ZDT1 GetInstance(int pd)
        {
            if (instance == null)
            {
                instance = new ZDT1(pd);
                instance.name = "ZDT1";
            }
            return instance;
        }
    }
}
