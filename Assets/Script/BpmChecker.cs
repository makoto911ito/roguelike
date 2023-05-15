using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

public class BpmChecker
{
    /// <summary></summary>
    const int _minBpm = 32;
    const int _maxBpm = 400;

    /// <summary>�W�{�����g���i�T���v�����O���g���j</summary>
    const int _baseFrequency = 44100;

    /// <summary>��{�I�ȃ`�����l����</summary>
    const int _baseCh = 2;

    /// <summary>�T���v���f�[�^�̊�{�����T�C�Y()</summary>
    const int _baseSplitSize = 2205;

    public struct BpmMatchData
    {
        public int bpm;
        public float match;
    }

    static BpmMatchData[] _bpmMatchDatas = new BpmMatchData[_maxBpm - _minBpm + 1];

    /// <summary>
    ///BPM�𒲂ׂ邽�߂̊֐�
    /// </summary>
    /// <param name="clip">����</param>
    /// <param name="frequency">�����̎��g��</param>
    /// <returns></returns>
    public static float AnalyzeBpm(AudioClip clip ,int frequency)
    {

        ////�^����ꂽ�����̎��g�����m�F
        //int _frequency = clip.frequency;
        //Debug.Log("���g�� :" + _frequency);

        //�^����ꂽ�����̃I�[�f�B�I�`�����l����
        int _channel = clip.channels;
        Debug.Log("�I�[�f�B�I�`�����l���� :" + _channel);


        //�t���[���T�C�Y�𒲂ׂ�i�^����ꂽ�����̎��g��/�x�[�X�̎��g��)*(�I�[�f�B�I �N���b�v�̃`�����l����/�x�[�X�̃`�����l��)*�X�v���b�g�T���v���T�C�Y
        int _flrameSize = Mathf.FloorToInt(((float)frequency / (float)_baseFrequency) * ((float)_channel / (float)_baseCh) * (float)_baseSplitSize);
        Debug.Log("�t���[���T�C�Y :" + _flrameSize);

        //audioclip ���炷�ׂẴT���v�� �f�[�^���擾����
        var _allSanples = new float[clip.samples * _channel];
        clip.GetData(_allSanples, 0);

        //
        var _allVolume = CreateVolume(_allSanples, frequency, _channel, _flrameSize);

        //�{�����[���[�z�񂩂�BPM������
        int _bpm = K(_allVolume, frequency, _flrameSize);
        Debug.Log("BPM :" + _bpm );

        return _bpm;
    }

    /// <summary>
    /// �{�����[���z��̍쐬�R�[�h
    /// </summary>
    /// <param name="allSunples">���ׂẴT���v���f�[�^</param>
    /// <param name="frequency">�����̎��g��</param>
    /// <param name="channel">�`�����l����</param>
    /// <param name="flrameSize">�t���[���T�C�Y</param>
    /// <returns></returns>
    private static float[] CreateVolume(float[] allSunples, int frequency, int channel, int flrameSize)
    {
        //�؂�グ
        var volume = new float[Mathf.CeilToInt((float)allSunples.Length / (float)flrameSize)];
        //Debug.Log(volume.Length);
        int Index = 0;

        for (int i = 0; i < allSunples.Length; i += flrameSize)
        {
            float sum = 0f;
            for (int frameIndex = i; frameIndex < i + flrameSize; frameIndex++)
            {

                if (allSunples.Length <= frameIndex)
                {
                    break;
                }

                //��Βl
                float adsValue = Mathf.Abs(allSunples[frameIndex]);
                if (adsValue > 1f)
                {
                    continue;
                }

                sum += (adsValue * adsValue);
            }

            //�������@�i��Βl/�t���[���T�C�Y�j
            volume[Index] = MathF.Sqrt(sum / flrameSize);
            Index++;
        }

        float maxVolume = volume.Max();
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i] = volume[i] / maxVolume;
        }

        return volume;
    }

    /// <summary>
    /// �K����BPM�̒l���o�����߂̊֐�
    /// </summary>
    /// <param name="allVolume">�{�����[���z��</param>
    /// <param name="frequency">�����̎��g��</param>
    /// <param name="frameSize">�t���[���T�C�Y</param>
    /// <returns></returns>
    private static int K(float[] allVolume, int frequency, int frameSize)
    {
        var list = new List<float>();

        for (int i = 1; i < allVolume.Length; i++)
        {
            list.Add(Mathf.Max(allVolume[i] - allVolume[i - 1], 0f));
        }

        int _index = 0;
        float splitFrequency = (float)frequency / (float)frameSize; //�������g��
        for (int bpm = _minBpm; bpm <= _maxBpm; bpm++)
        {
            float s = 0f;
            float c = 0f;
            float bps = (float)bpm / 60f;

            if(list.Count > 0)
            {
                for(int i =0; i < list.Count; i++)
                {
                    s += (list[i] * Mathf.Cos(i * 2f * Mathf.PI * bps / splitFrequency));
                    c += (list[i] * Mathf.Sin(i * 2f * Mathf.PI * bps / splitFrequency));
                }

                s *= (1f / (float)list.Count);
                c *= (1f / (float)list.Count);
            }

            float match = Mathf.Sqrt((s * s) + (c * c));

            _bpmMatchDatas[_index].bpm = bpm;
            _bpmMatchDatas[_index].match = match;
            _index++;
        }

        int matchIndex = Array.FindIndex(_bpmMatchDatas, x => x.match == _bpmMatchDatas.Max(y => y.match));

        return _bpmMatchDatas[matchIndex].bpm;
    }
}
