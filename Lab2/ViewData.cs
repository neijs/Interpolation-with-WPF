using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CLS_lib;
using Microsoft.Win32;

namespace SpringLab2
{
    public class ViewData : INotifyPropertyChanged
    {
        /* RawData Binding */

        private double boundA;
        public double BoundA
        {
            get { return boundA; }
            set { 
                boundA = value; 
                OnPropertyChanged();
            }
        }

        private double boundB;
        public double BoundB
        {
            get { return boundB; }
            set { 
                boundB = value; 
                OnPropertyChanged();
            }
        }

        private int nodeQnt;
        public int NodeQnt
        {
            get { return nodeQnt; }
            set { 
                nodeQnt = value; 
                OnPropertyChanged();
            }
        }
        
        private bool uniform;
        public bool Uniform
        {
            get { return uniform; }
            set
            {
                uniform = value;
                OnPropertyChanged();
            }
        }

        private List<FRaw> listFRaw;
        public List<FRaw> ListFRaw
        {
            get { return listFRaw; }
            set
            {
                listFRaw = value;
                OnPropertyChanged();
            }
        }

        private FRaw fRaw;
        public FRaw FRaw
        {
            get { return fRaw; }
            set
            {
                fRaw = value;
                OnPropertyChanged();
            }
        }

        private RawData? rawData;
        public RawData? RawData
        {
            get { return rawData; }
            set
            {
                rawData = value;
                OnPropertyChanged();
            }
        }

        private SplineData? splineData;
        public SplineData? SplineData
        {
            get { return splineData; }
            set
            {
                splineData = value;
                OnPropertyChanged();
            }
        }

        /* --------------- */

        /* SplineData Binding */
        private int nGrid;
        public int NGrid
        {
            get { return nGrid; }
            set
            {
                nGrid = value;
                OnPropertyChanged();
            }
        }

        private double leftDer;
        public double LeftDer
        {
            get { return leftDer; }
            set
            {
                leftDer = value;
                OnPropertyChanged();
            }
        }

        private double rightDer;
        public double RightDer
        {
            get { return rightDer; }
            set
            {
                rightDer = value;
                OnPropertyChanged();
            }
        }

        private SplineDataItem selectedItem;
        public SplineDataItem SelectedItem {
            get {return selectedItem;}
            set {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private string? interpolationBoundsText;
        public string? InterpolationBoundsText
        {
            get => this.interpolationBoundsText;
            set
            {
                this.interpolationBoundsText = value;
                OnPropertyChanged();
                OnInterpolationBoundsTextChanged();
            }
        }
        /* ------------------ */
        public Tuple<double, double>[]? NodeValue;
        public ViewData() {
            listFRaw = new List<FRaw>
            {
                RawData.Linear,     
                RawData.RandomInit,  
                RawData.Polynomial3
            };       
            fRaw = listFRaw[0];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnInterpolationBoundsTextChanged() {
            string[] bounds = this.InterpolationBoundsText!.Split(new[] {';', ',', ' ', '/', '-'}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (bounds.Length > 2) {
                    throw new ArgumentException("Found more than two values.", nameof(this.InterpolationBoundsText));
                }
                if (double.TryParse(bounds.First(), out double boundAValue)) {
                    this.BoundA = boundAValue;
                }
                if (double.TryParse(bounds.Last(), out double boundBValue)) {
                    this.BoundB = boundBValue;
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public void Save(string filename)
        {  
            rawData = new RawData(boundA, boundB, nodeQnt, uniform, fRaw);
            try {
                rawData.Save(filename);
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message);
            }
        }
        public void Load(string filename)
        {
            try {
                RawData.Load(filename, out rawData);
            } 
            catch (Exception exc) {
                MessageBox.Show(exc.Message);
            }
        }
        public void ExecuteSplinesFC() {
            try {
                rawData = new RawData(boundA, boundB, nodeQnt, uniform, fRaw);
                NodeValue = new Tuple<double, double>[rawData.nodeQnt];
                for (int i = 0; i < rawData.nodeQnt; ++i) {
                    NodeValue[i] = Tuple.Create(rawData.nodes[i], rawData.values[i]);
                }
                splineData = new SplineData(rawData, nGrid, leftDer, rightDer);
                splineData.DoSplines();
            } 
            catch (Exception exc) {
                MessageBox.Show(exc.Message);
            }
        }
        public void ExecuteSplinesFF() {
            try {
                NodeValue = new Tuple<double, double>[rawData!.nodeQnt];
                for (int i = 0; i < rawData!.nodeQnt; ++i) {
                    NodeValue[i] = Tuple.Create(rawData!.nodes[i], rawData!.values[i]);
                }
                splineData = new SplineData(rawData!, nGrid, leftDer, rightDer);
                splineData.DoSplines();
            } 
            catch (Exception exc) {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
