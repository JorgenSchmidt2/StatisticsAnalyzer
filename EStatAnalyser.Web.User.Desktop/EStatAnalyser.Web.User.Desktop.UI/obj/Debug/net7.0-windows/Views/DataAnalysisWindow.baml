   M S B A M L   `   `   `  ����\  XEStatAnalyser.Web.User.Desktop.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:EStatAnalyser.Web.User.Desktop.UI.ViewModels,EStatAnalyser.Web.User.Desktop.UI.ViewModels  5   	   ^ ZEStatAnalyser.Web.User.Desktop.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�}clr-namespace:EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities;assembly=EStatAnalyser.Web.User.Desktop.CoreBEStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities 5   	   @    :EStatAnalyser.Web.User.Desktop.UI.Views.DataAnalysisWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Llocal:clr-namespace:EStatAnalyser.Web.User.Desktop.UI.ViewModels     5   	   �core}clr-namespace:EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities;assembly=EStatAnalyser.Web.User.Desktop.Core     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	   $	��610��5   	   $	��920��6    � 
ResizeMode$ NoResize=�6"   $��1q�68   
���   � � DataContext 5      H   BEStatAnalyser.Web.User.Desktop.UI.ViewModels.DataAnalysisViewModel  5   
   .��5      � .+�5   
   �� $��9,9,0,0q�6   $
��Left=�6q   $	��Top=�6�   ���#111$��1��6�   ��6%   �� *ExternalFrameHeight+��6L   �� *ExternalFrameWidth+6�   (� 5   
    (� Stretch$ Uniform=�6   $
��Left=�6$   $	��Top=�6?   $��	10,10,0,0q�6W   ��6j   �� *ExternalCanvasWidth+��6�   �� *ExternalCanvasHeight+.��5      �� ��6   �� *	LabelData+��6=   �� *ExternalCanvasWidth+��6c   �� *ExternalCanvasHeight+��5      �� 5      .R�5      �� 5       � 	Resources 5       �� 
TargetType �� Property �� Value	 �� DataTypeR  LEStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities.TextLabel��%  '��       (�� v     5   #   *, +)�� 5      " ��6   .�5      ��  �@��Left$ 0��68   �� 5       �@��Bottom$ 0��6:   5      �� 5      "	  6#   .R�5      �� ��6    �� *LabelMargin+��6?   �� *	Content+��6[   �� *
FontSize+5       5!      5"      5#      �� 5%   
   $��	64,34,0,0q�6   $
��Left=�6s   $	��Top=�6�   ���#111$��2��6�   ��6'   �� *InternalFrameHeight+��6N   �� *InternalFrameWidth+6�   (� 5&   
   $��	65,35,0,0q�6   $ Uniform=�6%   $
��Left=�67   $	��Top=�6R   ��6j   �� *InternalCanvasWidth+��6�   �� *InternalCanvasHeight+.��5'      �� ��6   �� *
MainData+��6<   �� *InternalCanvasWidth+��6b   �� *InternalCanvasHeight+��5(      �� 5)      .R�5*      �� 5+       5-      O  IEStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities.Sphere��%[  '��       (�� v     53   #   *, +)�� 5.      " ��6   .�5/      ��  �@��Left$ 0��68   �� 50       �@��Bottom$ 0��6:   51      �� 53      "	  6#   .R�54      A� ��6"   �� *SphereMargin+��6B   �� *Color+��6[   �� *StrokeThicknessValue+��6�   �� *Radius+��6�   �� *Radius+55      56      57      58      (� 59   
   $��	65,35,0,0q�6   $ Uniform=�6%   $
��Left=�67   $	��Top=�6R   ��6j   �� *InternalCanvasWidth+��6�   �� *InternalCanvasHeight+.��5:      �� ��6   �� *	LinesData+��6=   �� *InternalCanvasWidth+��6c   �� *InternalCanvasHeight+��5;      �� 5<      .R�5=      �� 5>       5@      M  GEStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities.Line	
 �� X1	 �� X2	 �� Y1	 �� Y2��%[  '��       (�� s     5F   #   *, +)�� 5A      " ��6   .�5B      ��  �@��Left$ 0��68   �� 5C       �@��Top$ 0��67   5D      �� 5F      "	  6#   .R�5G      �� 
 6   �� *X1+ 61   �� *X2+ 6C   �� *Y1+ 6U   �� *Y2+��6g   �� *Color+��6�   �� *StrokeThicknessValue+5H      5I      5J      5K      �� 5M   
   $��
290,10,0,0q�6   $��30��6C   $	��210��6O   $
��Left=�6[   $	��Top=�6v   ��6$   �� *
XFieldName+6v   �� 5N   
   $��573,280,0,0q�6   $��30��6D   $��60��6P   $
��Left=�6[   $	��Top=�6v   ��6%   �� *
YFieldName+6v   �� 5P   
   $��
650,30,0,0q�6   $��30��6$   $	��210��60   $
��Left=�6<   $	��Top=�6W   $A��;Пороговое значение (z пороговое):��6o   �� 5Q   
   $��
650,60,0,0q�6.   $��20��6B   $	��220��6N   $
��Left=�6Z   $	��Top=�6u   ��6   �� *
ThresholdZ+6u   �� 5R   
   $��650, 90, 0, 0q�61   $ ��Отфильтровать��6H   $
��Left=�6`   $	��Top=�6{   $��30��6�   $	��100��6�   ��6   �� *ZFiltration+6�   �� 5S   
   $��770, 90, 0, 0q�65   $��Откатить��6L   $
��Left=�6_   $	��Top=�6z   $��30��6�   $	��100��6�   ��6   �� *ResetFiltration+6�   �� 5T   
   $��650, 130, 0, 0q�60   $+��%Построить регрессию��6H   $
��Left=�6f   $	��Top=�6�   $��30��6�   $	��220��6�   ��6   �� *
Regression+6�   �� 5V   
   $��650,250,0,0q�6   $	��130��6)   $	��220��66   $
��Left=�6B   $	��Top=�6]   ���#111$��1��6�   �� 5W   
   $��650,220,0,0q�6   $��30��6%   $	��210��61   $
��Left=�6=   $	��Top=�6X   $"��Данные анализа:��6p   �� 5X   
   $��651,251,0,0q�64   $	��128��6I   $	��218��6V   $
��Left=�6b   $	��Top=�6}   $
��Wrap=�6�   ��6   �� *AnalysisResult+6�   �� 5Z   
   $��650,420,0,0q�6   $��30��6%   $	��210��61   $
��Left=�6=   $	��Top=�6X   $%��Имя файла (отчёт):��6p   �� 5[   
   $��650,450,0,0q�6.   $��20��6C   $	��220��6O   $
��Left=�6[   $	��Top=�6v   ��6   �� *
ReportName+6v   �� 5\   
   $��650,480,0,0q�62   $6��0Сформировать отчёт (pdf + xlsx)��6G   $
��Left=�6q   $	��Top=�6�   $��30��6�   $	��220��6�   ��6   �� *CreateReport+6�   �� 5^   
   $��650, 536, 0, 0q�6*   $��Справка��6B   $
��Left=�6T   $	��Top=�6o   $��30��6�   $	��100��6�   ��6   �� *Help+6�   �� 5_   
   $��770, 536, 0, 0q�6+   $��Закрыть��6C   $
��Left=�6U   $	��Top=�6p   $��30��6�   $	��100��6�   ��6   �� *Close+6�   5a      5b      